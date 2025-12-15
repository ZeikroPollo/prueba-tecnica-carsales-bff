import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map, switchMap, forkJoin, of } from 'rxjs';
import { Episode } from '../models/episode.model';

export interface EpisodePage {
  items: Episode[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

@Injectable({ providedIn: 'root' })
export class EpisodesService {

  private readonly http = inject(HttpClient);

  getEpisodes(page = 1, name?: string): Observable<EpisodePage> {
    let params = new HttpParams().set('page', page);

    if (name?.trim()) {
      params = params.set('name', name.trim());
    }

    return this.http.get<any>('/api/episodes', { params }).pipe(
      map(res => {
        const items: Episode[] =
          Array.isArray(res.items) ? res.items :
          Array.isArray(res.episodes) ? res.episodes :
          Array.isArray(res.results) ? res.results :
          Array.isArray(res) ? res : [];

        const pageValue =
          res.page ??
          res.currentPage ??
          page;

        const pageSize =
          res.pageSize ?? items.length;

        const totalCount =
          res.totalCount ??
          res.totalItems ??
          res.info?.count ??
          items.length;

        const totalPages =
          res.totalPages ??
          res.info?.pages ??
          (pageSize > 0 ? Math.ceil(totalCount / pageSize) : 1);

        return {
          items,
          page: pageValue,
          pageSize,
          totalCount,
          totalPages
        };
      })
    );
  }

  // Carga TODAS las páginas y devuelve un solo array con todos los episodios
  getAllEpisodes(name?: string): Observable<Episode[]> {
    return this.getEpisodes(1, name).pipe(
      switchMap(firstPage => {
        const allItems: Episode[] = [...firstPage.items];

        if (firstPage.totalPages <= 1) {
          return of(allItems);
        }

        const requests: Observable<EpisodePage>[] = [];
        for (let p = 2; p <= firstPage.totalPages; p++) {
          requests.push(this.getEpisodes(p, name));
        }

        return forkJoin(requests).pipe(
          map(pages => {
            pages.forEach(p => allItems.push(...p.items));
            return allItems;
          })
        );
      })
    );
  }

  getEpisodeById(id: number): Observable<Episode> {
    return this.http.get<Episode>(`/api/episodes/${id}`);
  }
}