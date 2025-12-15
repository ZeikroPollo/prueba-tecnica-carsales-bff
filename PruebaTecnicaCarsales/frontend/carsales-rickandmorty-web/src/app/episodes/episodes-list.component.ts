import {
  Component,
  computed,
  inject,
  signal
} from '@angular/core';

import { NgIf, NgFor, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EpisodesService } from './episodes.service';
import { Episode } from '../models/episode.model';

@Component({
  selector: 'app-episodes-list',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, DatePipe],
  templateUrl: './episodes-list.component.html',
  styleUrl: './episodes-list.component.css'
})
export class EpisodesListComponent {

  private readonly episodesService = inject(EpisodesService);

  // Todos los episodios cargados del backend
  readonly episodes     = signal<Episode[]>([]);
  readonly loading      = signal<boolean>(false);
  readonly error        = signal<string | null>(null);

  // Filtros
  readonly searchTerm   = signal<string>('');
  readonly filterSeason = signal<string>('all');
  readonly sortAsc      = signal<boolean>(true);

  // Paginación CLIENTE
  readonly currentPage  = signal<number>(1);
  readonly pageSize     = signal<number>(10);  // episodios por página

  readonly hasLoaded    = signal<boolean>(false);

  // Episodios filtrados por texto + temporada
  readonly filteredEpisodes = computed(() => {
    const term   = this.searchTerm().toLowerCase();
    const season = this.filterSeason();
    const asc    = this.sortAsc();

    const filtered = this.episodes().filter(ep => {
      const matchesTerm =
        !term || ep.name.toLowerCase().includes(term);

      const matchesSeason =
        season === 'all' || ep.episodeCode.startsWith(season);

      return matchesTerm && matchesSeason;
    });

    return filtered.sort((a, b) =>
      asc
        ? a.episodeCode.localeCompare(b.episodeCode)
        : b.episodeCode.localeCompare(a.episodeCode)
    );
  });

  // Total de páginas según los FILTRADOS
  readonly totalPages = computed(() => {
    const count = this.filteredEpisodes().length;
    const size = this.pageSize();
    return count > 0 ? Math.ceil(count / size) : 1;
  });

  // Episodios de la página actual (CLIENTE)
  readonly pagedEpisodes = computed(() => {
    const page = this.currentPage();
    const size = this.pageSize();
    const all  = this.filteredEpisodes();

    const start = (page - 1) * size;
    const end   = start + size;

    return all.slice(start, end);
  });

  constructor() {
    this.loadAllEpisodes();
  }

  // ========= CARGA DE TODOS LOS EPISODIOS =========

  private loadAllEpisodes(): void {
    this.loading.set(true);
    this.error.set(null);

    this.episodesService.getAllEpisodes(this.searchTerm()).subscribe({
      next: (all) => {
        console.log('Total episodios cargados:', all.length);
        this.episodes.set(all);
        this.currentPage.set(1);   // siempre  en la página 1
        this.hasLoaded.set(true);
        this.loading.set(false);
      },
      error: err => {
        console.error('Error al cargar episodios', err);
        this.error.set('Ocurrió un error al cargar los episodios.');
        this.loading.set(false);
      }
    });
  }

  // ================== HANDLERS ==================

  onSearchChange(value: string): void {
    this.searchTerm.set(value);
    // recarga desde backend considerando el texto
    this.loadAllEpisodes();
  }

  onSeasonChange(value: string): void {
    this.filterSeason.set(value);
    this.currentPage.set(1);  // reset página cuando cambie temporada
  }

  toggleSort(): void {
    this.sortAsc.update(v => !v);
  }

  nextPage(): void {
    const next = this.currentPage() + 1;
    if (next <= this.totalPages()) {
      this.currentPage.set(next);
    }
  }

  prevPage(): void {
    const prev = this.currentPage() - 1;
    if (prev >= 1) {
      this.currentPage.set(prev);
    }
  }
}